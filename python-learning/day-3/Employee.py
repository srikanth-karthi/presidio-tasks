import re
from datetime import datetime
import pandas as pd
from fpdf import FPDF

class Employee:
    def __init__(self, name, dob, phone, email):
        self.name = name
        self.dob = datetime.strptime(dob, '%Y-%m-%d').date()
        self.phone = phone
        self.email = email
        self.age = self.get_age()

    def get_age(self):
        today = datetime.today()
        age = today.year - self.dob.year - ((today.month, today.day) < (self.dob.month, self.dob.day))

        return age

    def __str__(self):
        return f"Employee(name={self.name}, dob={self.dob.strftime('%Y-%m-%d')}, phone={self.phone}, email={self.email}, age={self.age})"

def validate_email(email):
    pattern = r'^[\w\.-]+@[a-zA-Z\d]+\.[a-zA-Z]{2,}$'
    return re.match(pattern, email) is not None

def validate_name(name):
    return len(name) > 3 and name.isalpha()

def validate_phno(phone):
    pattern = r"^\d{10}$"
    return re.match(pattern, phone) is not None

def validate_date(dob):
    try:
        datetime.strptime(dob, "%Y-%m-%d")
        return True
    except ValueError:
        return False

def get_employee_details():
    while True:
        name = input("Enter your name: ")
        if not validate_name(name):
            print("Invalid Name")
            continue
        break

    while True:
        email = input("Enter your email: ")
        if not validate_email(email):
            print("Invalid Email")
            continue
        break

    while True:
        phone = input("Enter your phone number: ")
        if not validate_phno(phone):
            print("Invalid Phone Number")
            continue
        break

    while True:
        dob = input("Enter your date of birth(yyyy-mm-dd): ")
        if not validate_date(dob):
            print("Invalid Date of Birth")
            continue
        break

    return Employee(name, dob, phone, email)

def save_to_text(employees, filename):
    if not filename.endswith(".txt"):
        filename += ".txt"
    with open(filename, 'w') as f:
        for emp in employees:
            f.write(f"Name: {emp.name}, DOB: {emp.dob}, Phone: {emp.phone}, Email: {emp.email}, Age: {emp.age}\n")

def save_to_excel(employees, filename):
    if not filename.endswith(".xlsx"):
        filename += ".xlsx"
    data = {
        "Name": [emp.name for emp in employees],
        "DOB": [emp.dob.strftime('%Y-%m-%d') for emp in employees], 
        "Phone": [emp.phone for emp in employees],
        "Email": [emp.email for emp in employees],
        "Age": [emp.age for emp in employees],
    }
    df = pd.DataFrame(data)
    df.to_excel(filename, index=False, engine='openpyxl')

def save_to_pdf(employees, filename):
    if not filename.endswith(".pdf"):
        filename += ".pdf"
    pdf = FPDF()
    pdf.add_page()
    pdf.set_font("Arial", size=12)
    for emp in employees:
        pdf.cell(200, 10, txt=f"Name: {emp.name}, DOB: {emp.dob.strftime('%Y-%m-%d')}, Phone: {emp.phone}, Email: {emp.email}, Age: {emp.age}", ln=True)
    pdf.output(filename)

def main():
    employees = []

    while True:
        print("\n1. Add Employee")
        print("2. Save to Text File")
        print("3. Save to Excel File")
        print("4. Save to PDF File")
        print("5. Bulk Read from Excel")
        print("6. Exit")
        choice = input("Enter your choice: ")

        if choice == '1':
            emp = get_employee_details()
            employees.append(emp)
            print(f"Employee {emp.name} added successfully.")

        elif choice == '2':
            filename = input("Enter filename (with .txt extension): ")
            save_to_text(employees, filename)
            print(f"Data saved to {filename}.")

        elif choice == '3':
            filename = input("Enter filename (with .xlsx extension): ")
            save_to_excel(employees, filename)
            print(f"Data saved to {filename}.")

        elif choice == '4':
            filename = input("Enter filename (with .pdf extension): ")
            save_to_pdf(employees, filename)
            print(f"Data saved to {filename}.")

        elif choice == '5':
            filename = input("Enter Excel filename to read from (with .xlsx extension): ")
            df = pd.read_excel(filename)
            for index, row in df.iterrows():
                emp = Employee(row['Name'], row['DOB'], row['Phone'], row['Email'])
                employees.append(emp)
                print(emp)
            print(f"Data read from {filename}.")

        elif choice == '6':
            break

        else:
            print("Invalid choice. Please try again.")

if __name__ == "__main__":
    main()
