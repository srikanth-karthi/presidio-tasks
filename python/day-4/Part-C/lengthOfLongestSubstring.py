class Solution:
    def lengthOfLongestSubstring(self, s: str) -> int:
        start = 0
        max_length = 0
        used_chars = {}
        
        for end in range(len(s)):
            if s[end] in used_chars and start <= used_chars[s[end]]:
                start = used_chars[s[end]] + 1
            else:
                max_length = max(max_length, end - start + 1)
            
            used_chars[s[end]] = end
        
        return max_length
