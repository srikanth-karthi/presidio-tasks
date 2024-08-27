-- 1) Print the storeid and number of orders for the store
select stor_id, count(stor_id) "Number of Orders"
from sales 
group by stor_id;

-- 2) print the numebr of orders for every title
select t.title, count(t.title) "Number of Orders"
from sales s
join titles t on s.title_id = t.title_id
group by t.title;

-- 3) print the publisher name and book name
select p.pub_name, t.title
from titles t
join publishers p on p.pub_id = t.pub_id;

-- 4) Print the author full name for all the authors
select concat(au_fname,' ', au_lname) "Full Name" from authors

-- 5) Print the price of every book with tax (price+price*12.36/100)
select title, price, price + (price * 12.36 / 100) "Price including tax"
from titles;

-- 6) Print the author name, title name
select concat(a.au_fname, ' ', a.au_lname) "Author Name", t.title
from authors a
join titleauthor ta on a.au_id = ta.au_id
join titles t on ta.title_id = t.title_id;

-- 7) print the author name, title name and the publisher name
select concat(a.au_fname, ' ', a.au_lname) as 'Author Name', t.title as Title, p.pub_name as Publisher
from authors a
join titleauthor ta on a.au_id = ta.au_id
join titles t on ta.title_id = t.title_id
join publishers p on t.pub_id = p.pub_id;

-- 8) Print the average price of books pulished by every publicher
select p.pub_name as Publisher, AVG(t.price) as 'Average price'
from titles t
join publishers p on t.pub_id = p.pub_id
group by p.pub_name;

-- 9) print the books published by 'Marjorie'
select t.title
from titles t
join publishers p on t.pub_id = p.pub_id
where p.pub_name = 'Marjorie';

-- 10) Print the order numbers of books published by 'New Moon Books'
select s.ord_num
from sales s
join titles t on s.title_id = s.title_id
join publishers p on t.pub_id = p.pub_id
where p.pub_name = 'New Moon Books';

-- 11) Print the number of orders for every publisher
select p.pub_name as Publisher, COUNT(s.ord_num) "Order Count"
from sales s
join titles t on s.title_id = t.title_id
join publishers p on t.pub_id = p.pub_id
group by p.pub_name;

-- 12) print the order number , book name, quantity, price and the total price for all orders
select s.ord_num, t.title "Book Name", s.qty, t.price, (s.qty * t.price) "Total price"
from sales s 
join titles t on s.title_id = t.title_id;

-- 13) print the total order quantity for every book
select t.title "Title", sum(s.qty) "Total Order Quantity"
from sales s 
join titles t on s.title_id = t.title_id
group by t.title;

-- 14) print the total ordervalue for every book
select t.title "Title", SUM(s.qty * t.price) "Total Order Value"
from sales s 
join titles t on s.title_id = t.title_id
group by t.title;

-- 15) print the orders that are for the books published by the publisher for which 'Paolo' works for
select s.*
from sales s
join titles t on s.title_id = t.title_id
join pub_info p on t.pub_id = p.pub_id
join employee e on p.pub_id = e.pub_id
where e.fname = 'Paolo';