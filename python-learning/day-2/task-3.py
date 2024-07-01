# 3) Sort sore and name of players print the top 10

players = [
    ("Alice", 50),
    ("Bob", 75),
    ("Charlie", 85),
    ("David", 40),
    ("Eva", 95),
    ("Frank", 60),
    ("Grace", 70),
    ("Hannah", 80),
    ("Ivy", 65),
    ("Jack", 55),
    ("Karen", 90),
    ("Leo", 45),
    ("Mona", 100)
]

sorted_players = sorted(players, key=lambda player: player[1], reverse=True)

top_10_players = sorted_players[:10]
for (name, score) in top_10_players:
    print(f"{name}: {score}")