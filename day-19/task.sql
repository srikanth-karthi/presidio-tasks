  
--1) Create a stored procedure that will take the author firstname and print all the books polished by him with the publisher's name


create proc proc_PrintAuthorsBooks(@firstname varchar(20))
as
begin
    select title from  titles t join titleauthor ta on t.title_id=ta.title_id join authors a on a.au_id=ta.au_id where a.au_fname=@firstname
end

EXEC proc_PrintAuthorsBooks 'Marjorie';



--2) Create a sp that will take the employee's firtname and print all the titles sold by him/her, price, quantity and the cost.
-- Alter the stored procedure

Alter proc proc_GetTitleSoldByEmployee(@firstName VARCHAR(50))
AS
BEGIN
SELECT
    t.title,
    t.price,
    SUM(s.qty) AS total_qty,
    (SUM(s.qty) * t.price) AS total_cost
FROM
    employee e
    JOIN publishers p ON e.pub_id = p.pub_id
    JOIN titles t ON p.pub_id = t.pub_id
    JOIN sales s ON s.title_id = t.title_id
WHERE
    e.fname = @firstName
GROUP BY
    t.title,
    t.price;

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

--print first 5 orders after sorting them based on the price of order

select t1.title, p.pub_name, t1.Name, t.price, sum(s.qty)'Quantity', sum(t.price*s.qty)'Total Price' from
(select t.title_id, t.title, STRING_AGG(Concat(a.au_fname ,' ', a.au_fname),', ') 'Name' from titles t join titleauthor ta on ta.title_id = t.title_id 
join authors a on ta.au_id=a.au_id group by t.title,t.title_id) as t1 join titles t on t1.title_id=t.title_id
join sales s on s.title_id = t.title_id
join publishers p on t.pub_id=p.pub_id
group by t1.title,p.pub_name,t1.Name,t.price order by sum(t.price*s.qty) desc;