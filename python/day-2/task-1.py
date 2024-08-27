# Longest Substring Without Repeating Characters.
# That is in a given string find the longest substring that does not contain any character twice.

def longest_substring_without_repeating(s):
    char_index = {}
    max_length = 0
    start = 0
    longest_substr = ""

    for i, char in enumerate(s):
        if char in char_index and char_index[char] >= start:
            start = char_index[char] + 1
        char_index[char] = i
        if i - start + 1 > max_length:
            max_length = i - start + 1
            longest_substr = s[start:i + 1]

    return longest_substr


print( longest_substring_without_repeating("abcaxabcd"))

