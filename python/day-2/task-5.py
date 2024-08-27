# 5) Credit card validation - Luhn check algorithm

def luhn_check(card_number):
    def digits_of(n):
        return [int(d) for d in str(n)]

    digits = digits_of(card_number)
    odd_digits = digits[-1::-2]
    even_digits = digits[-2::-2]

    checksum = 0
    checksum += sum(odd_digits)

    for d in even_digits:
        checksum += sum(digits_of(d * 2))

    return checksum % 10 == 0


card_number = input("Enter the credit card number: ").replace(" ", "")
if luhn_check(card_number):
    print("The credit card number is valid.")
else:
    print("The credit card number is invalid.")
