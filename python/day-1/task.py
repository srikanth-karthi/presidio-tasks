# #task-1
#
print("Hello World")
#
# #task-2
#
name = input("Enter your name: ")
print(f"Hello, {name}!")
#
# #task-3
#
name=input("Enter your name: ")
gender=input("Enter your gender(M/F): ")

if(gender.lower()=="m"):
    salutation ="Mr."
elif(gender.lower()=="f"):
    salutation ="Ms."
else:
    salutation=""

print(f"{salutation}{name}")


# #task-4
#
# # Take name, age, date of birth, and phone, then print details in proper format
#
name = input("Enter your name: ")
age = input("Enter your age: ")
date = input("Enter your date of birth: ")
phone=input("Enter your phone number: ")

print(f"Your name is {name}\nYour age is {age}\nYour date of birth is {date}\nYour phone number is {phone}")



#task-5
# Add validation to the entered name, age, date of birth, and phone, then print details in proper format

def validate_name(name):
    if len(name) == 0:  # Check if the name is empty
        return False
    elif not name.isalpha():  # Check if the name contains only alphabetic characters
        return False
    else:
        return True

def validate_age(age):
    if age.isdigit() and 0 <= int(age) <= 120:  # Age should be a number between 0 and 120
        return True
    else:
        return False

def validate_dob(dob):
    try:
        day, month, year = map(int, dob.split('/'))
        if 1 <= day <= 31 and 1 <= month <= 12 and year > 1900:
            return True
        else:
            return False
    except ValueError:
        return False

def validate_phone(phone):
    if phone.isdigit() and len(phone) == 10:  # Phone number should be a 10-digit number
        return True
    else:
        return False

name = input("Enter your name: ")
while not validate_name(name):
    print("Invalid name. Please enter alphabetic characters only and it should not be empty.")
    name = input("Enter your name: ")

age = input("Enter your age: ")
while not validate_age(age):
    print("Invalid age. Please enter a number between 0 and 120.")
    age = input("Enter your age: ")

dob = input("Enter your date of birth (DD/MM/YYYY): ")
while not validate_dob(dob):
    print("Invalid date of birth. Please enter in the format DD/MM/YYYY.")
    dob = input("Enter your date of birth (DD/MM/YYYY): ")

phone = input("Enter your phone number: ")
while not validate_phone(phone):
    print("Invalid phone number. Please enter a 10-digit number.")
    phone = input("Enter your phone number: ")

print(f"\nDetails:\nName: {name}\nAge: {age}\nDate of Birth: {dob}\nPhone: {phone}")

#task-6
# Find if the given number is prime

def check_prime(num):
    if num <=1:
        return False
    else:
        for i in range(2,(num//2)+1):
            if num%i==0:
                return False

        return True

if check_prime(int(input('Enter a number: '))):
    print('its a Prime')
else:
    print('its not a Prime')


#task-7
# Take 10 numbers and find the average of all the prime numbers in the collection


prime_numbers=[]
for _ in range(10):
   n= int(input("enter a number"))
   if check_prime(n):
       prime_numbers.append(n)

print(F"the average of prime numbers are {sum(prime_numbers)/len(prime_numbers)}")


#task-8
 # Length of a given input string

print(F"the length of the string is {len(input("Enter a string"))}");



#task-9
# Find All Permutations of a given string

def all_permutations(string):
    if len(string) <= 1:
        return [string]

    permutations = []
    for i in range(len(string)):
        char = string[i]
        remaining_chars = string[:i] + string[i+1:]
        for permutation in all_permutations(remaining_chars):
            permutations.append(char + permutation)
    return permutations

input_string = input("Enter a string: ")
permutations = all_permutations(input_string)
print(f"Given string has {len(permutations)} permutations")
for perm in permutations:
    print(perm)

# task-10
# Print a pyramid of stars for the number of rows specified

rows = int(input("Enter the number of rows: "))

for i in range(rows):
    print(' ' * (rows - i - 1) + '*' * (2 * i + 1))
