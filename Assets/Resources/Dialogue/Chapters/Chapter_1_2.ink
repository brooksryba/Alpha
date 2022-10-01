-> chapter_1_2_intro


=== chapter_1_2_intro ===
# Spawn("MF", 8, -13) # Spawn("AF", 4, -13) 
# MoveMultiple("Player", {{6,-11}}) 

Hero: Hey guys, how's the training going? # Indicator("Player")

Hero: How do you guys know what your combat affinities will be?

AF: My dad was an archer in the Great War. Growing up my mom<br>always told me stories of his skills and bravery. # Indicator("AF")
AF: I've always wanted to be one ever since, it also just feels<br>natural to use a bow. What about you MF? 
AF: What about you MF?!

MF: Oh, ugh, sorry about that. I was reading about this cool spell.<br>What was the question again? # Indicator("MF")

Hero: How do you know what your combat affinity will be? # Indicator("Player")

MF: I've always enjoyed reading and learning spells.<br>I think it's part of my nature. Why do you ask? # Indicator("MF")

Hero: The affinity test is tomorrow and I'm worried because there<br>isn't just one combat style that feels right to me. # Indicator("Player")
Hero: My grandma tells me that my dad was a warrior and my mom was a mage.
Hero: She even said my grandpa was an amazing archer.I've tried the different styles<br>and I'm OK with each but I'm not sure which one is the right one for me. 

AF: Don't worry about it too much. The whole point of the affinity stone<br>is to let the person know which combat style they will excel at. # Indicator("AF")

MF: She's right. They say the stone is able to look into a person's soul<br>and discover their inner attributes. # Indicator("MF")
MF: As far as I know it hasn't been wrong about anyone.

Hero: I'm sure you guys are right. Regardless of what I get, I'll embrace it. # Indicator("Player")

AF: Look over there. # Indicator("AF")

# MoveMultiple("Player", {{3, -11}, {3,-16}}) # MoveMultiple("AF", {{1, -13},{1,-18}}) # MoveMultiple("MF", {{5, -13}, {5,-18}}) # Spawn("Rat", 3,-18)
AF: Look over there.

AF: Let's get some real practice! # Indicator("AF")

Fight the rats? 
    * [Yes]
        # Battle("Rat", "chapter_1_2_post_rats")
        Hero: Yes, they will be great practice! # Indicator("Player")
        -> chapter_1_2_post_rats
    * [No]
        Hero: No, let's leave them be. # Indicator("Player")
        -> chapter_1_2_no_rats


=== chapter_1_2_post_rats ===
# Spawn("MF", 5, -18) # Spawn("AF", 1, -18) # Spawn("Livar", 3, -1) # MoveMultiple("Livar", {{3,-15}}) # Spawn("Murray", 2, 0) # MoveMultiple("Murray", {{2,-14}}) # Spawn("Stormy", 4, 0) # MoveMultiple("Stormy", {{4,-14}})
Livar: Not bad, but you still need more practice. # Indicator("Livar")
-> chapter_1_2_pre_rival

=== chapter_1_2_no_rats ===
# Move("Rat", 3, -25)
AF: Shoo rat! # Indicator("AF")

# Spawn("Livar", 3, -1) # MoveMultiple("Livar", {{3,-15}}) # Spawn("Murray", 2, 0) # MoveMultiple("Murray", {{2,-14}, {1, -14}}) # Spawn("Stormy", 4, 0) # MoveMultiple("Stormy", {{4,-14}, {5, -14}})
Livar: Do you think you are above the practice? # Indicator("Livar")
-> chapter_1_2_pre_rival

=== chapter_1_2_pre_rival ===
Hero: If you think you're so good Livar, let's<br>have a duel to prove it! # Indicator("Player")

Livar: I accept your challenge! # Indicator("Livar")

AF: I know we normally break these fights up, but<br>perhaps a friendly spar could benefit us all. # Indicator("AF")

# Battle("Livar", "chapter_1_2_post_rival")
MF: The experience would help out for combat school. # Indicator("MF")
-> chapter_1_2_post_rival

=== chapter_1_2_post_rival ===
# Spawn("MF", 5, -18) # Spawn("AF", 1, -18) # Spawn("Livar", 3, -15) # Spawn("Murray", 2, -14) # Spawn("Stormy", 4, -14)
All: Good match!

Livar: You guys got lucky... # Indicator("Livar")

# MoveMultiple("Livar", {{3, -10}}) # MoveMultiple("Murray", {{2, -10}}) # MoveMultiple("Stormy", {{4, -10}})
Hero: What a sore loser. # Indicator("Player")

MF: That's enough training for me today. # Indicator("MF")

AF: Same here, we should get some rest for tomorrow. # Indicator("AF")

# Move("MF", 5, -8) # Move("AF", 1, -8)
Hero: You're right, I'll see you guys in the morning. # Indicator("Player")


-> DONE



