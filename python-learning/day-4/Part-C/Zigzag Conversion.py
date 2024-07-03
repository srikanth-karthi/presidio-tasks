class Solution:
    def convert(self, s: str, numRows: int) -> str:
        if numRows == 1 or numRows >= len(s):
            return s
        
        rows = [''] * numRows
        index = 0
        direction = 1  
        
        for char in s:
            rows[index] += char
            if index == 0:
                direction = 1  
            elif index == numRows - 1:
                direction = -1 
            
            index += direction
            print(rows)
        return ''.join(rows)