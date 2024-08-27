# 2) Print the list of prime numbers up to a given number

def find_primes_up_to(n):
    primes = []
    for num in range(2, n + 1):
        is_prime = True
        for i in range(2, int(num ** 0.5) + 1):
            if num % i == 0:
                is_prime = False
                break
        if is_prime:
            primes.append(num)
    return primes


max_number = int(input("Enter a number: "))
prime_numbers = find_primes_up_to(max_number)

print(f"Prime numbers up to {max_number}: {prime_numbers}")