-> chapter_1_1_intro


=== chapter_1_1_intro ===
# Spawn("MF", 26, 8) # Spawn("AF", 26, -2) # Spawn("Livar", 38, 7)
# MoveMultiple("MF", {{26,5}, {24,5}}) # MoveMultiple("AF", {{26,3}, {24,3}})

Hero: How do you guys know what your combat affinities will be? # Indicator("Player")

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

AF: Look over there. Let's get some real practice! # Indicator("AF")

Fight the rats? 
    * [Yes]
        # Battle("Rat", "chapter_1_1_post_rats")
        Hero: Yes, they will be great practice! # Indicator("Player")
        -> chapter_1_1_post_rats
    * [No]
        Hero: No, let's leave them be. # Indicator("Player")
        -> chapter_1_1_no_rats


=== chapter_1_1_post_rats ===
# Spawn("MF", 24, 5) # Spawn("AF", 24, 3) # Spawn("Livar", 38, 7) # MoveMultiple("Livar", {{31,4}, {26,4}})
Livar: Not bad, but you still need more practice. # Indicator("Livar")
-> chapter_1_1_pre_rival

=== chapter_1_1_no_rats ===
# Spawn("MF", 24, 5) # Spawn("AF", 24, 3) # Spawn("Livar", 38, 7) # MoveMultiple("Livar", {{31,4}, {26,4}})
Livar: Do you think you are above the practice? # Indicator("Livar")
-> chapter_1_1_pre_rival

=== chapter_1_1_pre_rival ===
Hero: If you think you're so good Livar, let's<br>have a duel to prove it! # Indicator("Player")

Livar: I accept your challenge! # Indicator("Livar")

AF: I know we normally break these fights up, but<br>perhaps a friendly spar could benefit us all. # Indicator("AF")

# Battle("Livar", "chapter_1_1_post_rival")
MF: The experience would help out for combat school. # Indicator("MF")
-> chapter_1_1_post_rival

=== chapter_1_1_post_rival ===
# Spawn("MF", 24, 5) # Spawn("AF", 24, 3) # Spawn("Livar", 26, 4)
All: Good match!

MF: That's enough training for me today. # Indicator("MF")

AF: Same here, we should get some rest for tomorrow. # Indicator("AF")

Hero: You're right, I'll see you guys in the morning. # Indicator("Player")

-> DONE



