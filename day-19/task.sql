  
--1) Create a stored procedure that will take the author firstname and print all the books polished by him with the publisher's name


create proc proc_PrintAuthorsBooks(@firstname varchar(20))
as
begin
    select title from  titles t join titleauthor ta on t.title_id=ta.title_id join authors a on a.au_id=ta.au_id where a.au_fname=@firstname
end

EXEC proc_PrintAuthorsBooks 'Marjorie';



--2) Create a sp that will take the employee's firtname and print all the titles sold by him/her, price, quantity and the cost.
-- Alter the stored procedure

create proc proc_GetTitleSoldByEmployee(@firstName VARCHAR(50))
AS
BEGIN
    SELECT
        t.title,
        t.price,
        s.qty,
        (s.qty * t.price) AS cost
    FROM
        employee e
        JOIN publishers p ON e.pub_id = p.pub_id
        JOIN titles t ON p.pub_id = t.pub_id
        JOIN sales s ON s.title_id = t.title_id
    WHERE
        e.fname = @firstName;
END

-- Call the stored procedure
EXEC proc_GetTitleSoldByEmployee 'Paolo';



--) Create a query that will print all names from authors and employees

 SELECT 
    e.fname AS EmployeeName,
    a.au_fname AS AuthorName
FROM 
    employee e
    JOIN publishers p ON e.pub_id = p.pub_id
    JOIN titles t ON p.pub_id = t.pub_id
    JOIN titleauthor ta ON t.title_id = ta.title_id
    JOIN authors a ON ta.au_id = a.au_id;



--4) Create a  query that will float the data from sales,titles, publisher and authors table to print title name, Publisher's name, author's full name with quantity ordered and price for the order for all orders,

SELECT 
    t.title AS [Book title],
    p.pub_name AS [Publisher's name],
    a.au_fname AS AuthorName,
    SUM(s.qty) AS Quantity,
    SUM(s.qty * t.price) AS Price
FROM 
    publishers p
    JOIN titles t ON p.pub_id = t.pub_id
    JOIN titleauthor ta ON t.title_id = ta.title_id
    JOIN authors a ON ta.au_id = a.au_id
    JOIN sales s ON s.title_id = t.title_id
GROUP BY 
    t.title, p.pub_name, a.au_fname;


--print first 5 orders after sorting them based on the price of order

SELECT TOP 5 
    s.*,
    SUM(t.price * s.qty) AS TotalPrice
FROM 
    sales s
    JOIN titles t ON t.title_id = s.title_id
GROUP BY 
    s.ord_num, s.title_id, s.qty, s.payterms, s.ord_date, s.stor_id
ORDER BY 
    TotalPrice desc;
