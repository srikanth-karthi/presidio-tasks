class Solution:
    def multiply(self, num1: str, num2: str) -> str:
        a = 0
        for i in range(len(num1)):
            a = a*10 + int(num1[i])
        
        b = 0
        for i in range(len(num2)):
            b = b*10 + int(num2[i])

        return str(a * b)
