# Copyright (C) Sylvia Oleander-Rothove 2022
# awawawa

f = open("relics.txt", "r")
raw = f.read()

li = raw.split("\n")

newConfigPairs = []

for string in li:
    if string.find("//") != -1 or string == "":
        continue

    stringElements = string.split(" ")

    className = stringElements[2]

    toAppend = "relicConfigPairs.Add(new RelicConfigPair(new {cn}(), SpireConfig.enable{cn}.Value));".format(cn = className)
    
    newConfigPairs.append(toAppend)


for string in newConfigPairs:
    print(string)

        
