# 4) Application to play the Cow and Bull game maintain score as well. - reff - Wordle of New York Times

import random

def generate_secret_word(word_list):
    return random.choice(word_list)


def get_cows_and_bulls(secret_word, guess):
    cows = 0
    bulls = 0
    for i in range(len(secret_word)):
        if guess[i] == secret_word[i]:
            bulls += 1
        elif guess[i] in secret_word:
            cows += 1
    return cows, bulls


def play_game(word_list, max_attempts=10):
    secret_word = generate_secret_word(word_list)
    attempts = 0
    score = 0

    print("Welcome to the Cow and Bull game!")
    print("Try to guess the secret word.")

    while attempts < max_attempts:
        guess = input(f"Attempt {attempts + 1}/{max_attempts}: ").lower()
        if len(guess) != len(secret_word):
            print(f"Please enter a {len(secret_word)}-letter word.")
            continue

        attempts += 1
        cows, bulls = get_cows_and_bulls(secret_word, guess)
        print(f"Cows: {cows}, Bulls: {bulls}")

        if bulls == len(secret_word):
            print(f"Congratulations! You guessed the word '{secret_word}' in {attempts} attempts.")
            score = max_attempts - attempts + 1
            break
    else:
        print(f"Sorry, you've used all {max_attempts} attempts. The secret word was '{secret_word}'.")

    print(f"Your score: {score}")


word_list = ["apple", "banana", "cherry", "mango", "orange", "grapes"]
play_game(word_list)