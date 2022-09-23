# Copyright (C) Sylvia Oleander-Rothove 2022
# awawawa

f = open("relics.txt", "r")
raw = f.read()

li = raw.split("\n")

newInitializers = []
newConfigBindings = []

for string in li:
    if string.find("//") != -1 or string == "":
        continue

    stringElements = string.split(" ")

    configName = "enable" + stringElements[2]
    #newInitializers.append("public static ConfigEntry<bool> " + configName + ";")
    newConfigBindings.append(configName + " = config.Bind(\"Toggles\", \"" + configName + "\", true, \"Set to true to enable " +
                             stringElements[2] + ".\");")


for string in newConfigBindings:
    print(string)
        
