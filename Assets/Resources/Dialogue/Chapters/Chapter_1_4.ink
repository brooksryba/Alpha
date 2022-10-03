-> chapter_1_3_intro


=== chapter_1_3_intro ===
# Spawn("AF", 13, -22) # Spawn("MF", 14, -22) 
# Move("Player", 13, -21)
AF: The blacksmith is right down here. # Indicator("AF")

# Spawn("Warrior", 20, -26) # Spawn("Knight", 21, -26)
# MoveMultiple("Player", {{13,-28}, {17, -28}})
# MoveMultiple("AF", {{13,-27}, {17, -27}})
# MoveMultiple("MF", {{14,-26}, {17, -26}})
AF: The blacksmith is right down here.

AF: Ugh! This is going to take so long!

# Spawn("Archer", 18, -25)
# Move("Archer", 18, -26)
# MoveMultiple("Warrior", {{18, -26}, {18, -25}})
# Move("Knight", 20, -26)
# Unspawn("Warrior")
Unknown Warrior: I can't believe I had a mage affinity. # Indicator("Archer")

Unknown Warrior: Everyone in my family has been an archer for generations!

Unknown Warrior: I've already practiced so much with my bow.

# MoveMultiple("Archer", {{18,-29}, {5, -29}})
AF: Poor guy, hopes it turns out well for him  # Indicator("AF")

MF: I don't know why he's so sad, mage affinity is the best! # Indicator("MF")

# Spawn("Livar", 19, -25)
# Spawn("Murray", 18, -25)
# Spawn("Stormy", 18, -25)
# MoveMultiple("Livar", {{19, -26}})
# MoveMultiple("Murray", {{18, -26}})
# MoveMultiple("Stormy", {{18, -27}})
Livar: I knew I would be a warrior! # Indicator("Livar")

# MoveMultiple("Livar", {{19, -29}, {4, -29}})
Murray: We got the affinities we were hoping for! # Indicator("Murray")

Stormy: I hope you guys get the affinities that you wanted! # Indicator("Stormy")

AF: Thanks! # Indicator("AF")

MF: Thank you guys! # Indicator("MF")

Murray: We better go catch up with Livar! # Indicator("Murray")

# MoveMultiple("Murray", {{18, -29}, {3, -29}})
# MoveMultiple("Stormy", {{18, -29}, {2, -29}})
Hero: I can't believe those guys are friends with Livar. # Indicator("Player")

Hero: They're so nice and he's such a jerk!

MF: He's actually quite nice. # Indicator("MF")

AF: He is when ever Hero isn't around. I don't know what it is but both<br>of you completely lose your cool when the other is around. # Indicator("AF")

Hero: I've tried to get along with him but he's so arrogant. # Indicator("Player")

Hero: Whatever though.... looks like we're up for the test. You guys should go first.

# MoveMultiple("AF", {{18,-27}, {18, -25}})
# MoveMultiple("MF", {{18,-26}, {18, -25}})
# MoveMultiple("Knight", {{18, -26}, {18, -25}})
# Unspawn("AF")
# Unspawn("MF")
# Unspawn("Knight")
Hero: Whatever though.... looks like we're up for the test. You guys should go first.

-> DONE