-> chapter_1_3_intro


=== chapter_1_3_intro ===
# Spawn("AF", 15, -11)
# MoveMultiple("Player", {{6,-12}, {14, -12}})
Hero: Hey! There's AF! # Indicator("Player")

AF: Hi Hero! Today's the big day! I can't wait for the test and to<br> start orientation at combat school! # Indicator("AF")

AF: Are you still nervous about the test?
    * [Yes]
        Hero: Yes, I'm not sure what to expect.<br> Regardless of what happens I'll make the best of it. # Indicator("Player")
        -> chapter_1_3_mf
    * [No]
        Hero: Not anymore. I realized it doesn't matter what affinity I get.<br> Regardless of what happens I'll make the best of it. # Indicator("Player")
        -> chapter_1_3_mf

=== chapter_1_3_mf ==
AF: That's the spirit! Now let's go get MF. # Indicator("AF")

# Spawn("MF", 27, -11)
# MoveMultiple("Player", {{26,-12}}) # MoveMultiple("AF", {{26,-11}})
AF: That's the spirit! Now let's go get MF.

MF: I can't wait to learn more spells! # Indicator("MF")

AF: I would be worried if you didn't! # Indicator("AF")

Hero: Let's head to the blacksmith and do the affinity test! # Indicator("Player")

# MoveMultiple("AF", {{11,-11}}) # MoveMultiple("MF", {{12,-11}})
Hero: Let's head to the blacksmith and do the affinity test!

-> DONE