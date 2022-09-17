# Copyright (C) Sylvia Oleander-Rothove 2022
# awawawa

f = open("configEntries.txt", "r")
raw = f.read()

li = raw.split("\n")

newToggles = []

for string in li:
    if string.find("//") != -1 or string == "":
        continue

    stringElements = string.split(" ")

    configName = "toggles.Add(" + stringElements[3][:-1] + ");"
    newToggles.append(configName)


for string in newToggles:
    print(string)

        
