
# 1) Print hello world

# 2) Take a name and print greet

# 3) Take name and gender print greet with salutation

# 4) Take name, age, date of birth and phone print details in proper format

# 5) Add validation the entered  name, age, date of birth and phone print details in proper format

# 6) Find if the given number is prime

# 7) Take 10 numbers and find the average of all the prime numbers in the collection

# 8) Length of a given input string

# 9) Find All Permutations of a given string

# 10) Print a pyramid of starts for the number of rows specified

#    *

#   ***

# ******


# 9) Find All Permutations of a given string


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