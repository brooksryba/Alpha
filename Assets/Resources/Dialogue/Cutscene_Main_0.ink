-> chapter_1_intro


=== chapter_1_intro ===

# Move("MF", 23.5, 4.5) # Move("AF", 23.5, 3.0)

Hero: What's your name?

AF: I'm Archer Friend. Lets join parties!

Hero: How do you guys know what your combat affinities will be?

AF: My dad was an archer in the Great War. Growing up my mom<br>always told me stories of his skills and bravery. I've always<br>wanted to be one ever since, it also just feels natural to <br>use a bow. What about you MF?

AF: What about you MF?!

MF: Oh, ugh, sorry about that. I was reading about this cool spell.<br>What was the question again?

Hero: How do you know what your combat affinity will be?

MF: I've always enjoyed reading and learning spells.<br>I think it's part of my nature. Why do you ask?

Hero: The affinity test is tomorrow and I'm worried because there<br>isn't just one combat style that feels right to me. My grandma<br>tells me that my dad was a warrior and my mom was a mage. She even<br>said my grandpa was an amazing archer.I've tried the different styles<br>and I'm OK with each but I'm not sure which one is the right one for me.

AF: Don't worry about it too much. The whole point of the affinity stone<br>is to let the person know which combat style they will excel at.

MF: She's right. They say the stone is able to look into a person's soul<br>and discover their inner attributes. As far as I know it hasn't been wrong about anyone.

Hero: I'm sure you guys are right. Regardless of what I get, I'll embrace it.

AF: Look over there. Let's get some real practice!

Fight the rats?
    * [Yes]
        Hero: Yes, they will be great practice!
        // trigger battle scene
        -> chapter_1_post_rats
    * [No]
        Hero: No, let's leave them be.
        -> chapter_1_no_rats


=== chapter_1_post_rats ===
Livar: Not bad, but you still need more practice.
-> chapter_1_pre_rival

=== chapter_1_no_rats ===
Livar: Do you think you are above the practice?
-> chapter_1_pre_rival

=== chapter_1_pre_rival ===
Hero: If you think you're so good Livar, let's have a duel to prove it!

Livar: I accept your challenge!

AF: I know we normally break these fights up, but perhaps a friendly spar could benefit us all.

MF: The experience would help out for combat school.

// trigger battle scene

All: Good match!

MF: That's enough training for me today.

AF: Same here, we should get some rest for tomorrow.

Hero: You're right, I'll see you guys in the morning.

-> DONE



