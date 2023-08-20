-> chapter_1_2_intro


=== chapter_1_2_intro ===
# Spawn("MF", -8, -1) # Spawn("AF", -4, -1) 
# MoveMultiple("Player", {{1,6},{1,2},{-6,2},{-6,1}}) 

Hero: Hey guys, how's the training going? # Indicator("Player")

AF: Great! # Indicator("AF")

MF: **Keeps staring down at his book** # Indicator("MF")

Hero: How do you guys know what your combat affinities will be? # Indicator("Player")

AF: My dad was an archer in the Great War. Growing up my mom<br>always told me stories of his skills and bravery. # Indicator("AF")
AF: I've always wanted to be one ever since, it also just feels<br>natural to use a bow. What about you MF? 

MF: .......  # Indicator("MF")

AF: What about you MF?!# Indicator("AF")

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

AF: Look over there, a giant rat!. # Indicator("AF")

Hero: Let's check it out! # Indicator("Player")

# MoveMultiple("Player", {{-3,1}, {-3,-4.5}}) # MoveMultiple("MF", {{-5, -1}, {-5,-4}}) # MoveMultiple("AF", {{-1, -1},{-1,-4}})  # Spawn("Rat", -3,-6)
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
# Spawn("MF", -5, -4) # Spawn("AF", -1, -4) # Spawn("Livar", -3, -10) # MoveMultiple("Livar", {{-3,-6}}) # Spawn("Murray", -5, -10) # MoveMultiple("Murray", {{-5,-7}}) # Spawn("Stormy", -1, -10) # MoveMultiple("Stormy", {{-1,-7}})
Livar: Not bad, but you still need more practice. # Indicator("Livar")
-> chapter_1_2_pre_rival

=== chapter_1_2_no_rats ===
# Spawn("MF", -5, -4) # Spawn("AF", -1, -4) 
AF: Shoo rat! # Indicator("AF")
# Move("Rat", -3, -10)
MF: That was a big ugly rat! # Indicator("MF")
# Unspawn ("Rat")


# Spawn("Livar", -3, -10) # MoveMultiple("Livar", {{-3,-6}}) # Spawn("Murray", -5, -10) # MoveMultiple("Murray", {{-5,-7}}) # Spawn("Stormy", -1, -10) # MoveMultiple("Stormy", {{-1,-7}})
Livar: So you think you're above the practice? # Indicator("Livar")
-> chapter_1_2_pre_rival

=== chapter_1_2_pre_rival ===
Hero: If you think you're so good Livar, let's<br>have a duel to prove it! # Indicator("Player")

Livar: I accept your challenge! # Indicator("Livar")

AF: I know we normally break these fights up, but<br>perhaps a friendly spar could benefit us all. # Indicator("AF")

# Battle("Livar", "chapter_1_2_post_rival")
MF: The experience would help out for combat school. # Indicator("MF")
-> chapter_1_2_post_rival

=== chapter_1_2_post_rival ===
# Spawn("MF", -5,-4) # Spawn("AF", -1, -4) # Spawn("Livar", -3, -6) # Spawn("Murray", -5, -7) # Spawn("Stormy", -1, -7)
All but Livar: Good match! #Indicator ("Player")

Livar: You guys got lucky... #Indicator("Livar")

Livar: You won't be so lucky next time!

Hero: Quit being a bad sport cry baby! # Indicator("Player")

Livar: Let's go guys! I can't be around this chompsky any longer. # Indicator("Livar")

# MoveMultiple("Livar", {{1, -6},{1,0}})

# Unspawn("Livar")

Stormy: Livar... # Indicator("Stormy")

Murray: He'll be fine. # Indicator("Murray")

Stormy: Well, we'll see you guys tomorrow! # Indicator("Stormy")

Murray: See you at Orientation! # Indicator("Murray")

AF: Later! # Indicator("AF")

MF: Bye! # Indicator("MF")

# MoveMultiple("Murray", {{1, -8},{1,1}}) # MoveMultiple("Stormy", {{1, -7},{1,1}})

# Unspawn("Murray") #Unspawn("Stormy")

Hero: Bye guys. # Indicator("Player")

AF: You didn't have to call him a cry baby! # Indicator("AF")

Hero: Well he was being a bad sport! # Indicator("Player")

MF: He was, yes, but name-calling is not sportsman like either. # Indicator("MF")

Hero: I guess you guys are right. He's still a jerk though. # Indicator("Player")

AF: I don't understand why you guys hate each other so much. # Indicator("AF")

AF: It's been like this ever since you were kids.

AF: I guess it can't be helped. I'm going to go rest up for tomorrow.

AF: You guys should get some rest too.

MF: Indeed. I've had enough training today. # Indicator("MF")

# Indicator("Player") 

Hero: You're right, I'll see you guys in the morning. 

# MoveMultiple("MF", {{-5, 1}}) # MoveMultiple("AF", {{-1, 1}})

# Unspawn("AF") #Unspawn("MF")

-> DONE



