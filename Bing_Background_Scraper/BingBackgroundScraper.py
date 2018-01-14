import requests
from bs4 import BeautifulSoup
import re
import urllib.request
import shutil
import datetime
import os

# Collect and parse first page
page = requests.get('https://www.bing.com/HPImageArchive.aspx?format=xml&idx=0&n=1&mkt=en-CA')
soup = BeautifulSoup(page.text, 'html.parser')


#print (soup.prettify())
print("XML Page retreived")
newPath = 'bingXML.txt'
newFile = open(newPath,'w')
newFile.write(soup.prettify())
newFile.close()
print("XML File saved as text")
foundFlag = 0
foundURL = ""
ptr = r'<url>'
print("Parsing file...")
with open('bingXML.txt', 'r') as file:
    for line in file:
        #print(line)
        if foundFlag == 1:
            #print(line)
            foundURL = line
            foundFlag = 0
        for match in re.finditer(ptr, line):
            print("URL tag found:")
            foundFlag = 1

foundURL = foundURL.strip()
url = "https://www.bing.ca" + foundURL
print(url)

foundFlag2 = 0
ptr2 = r'<copyright>'
with open('bingXML.txt', 'r') as file:
    for line2 in file:
        #print(line)
        if foundFlag2 == 1:
            print(line2.strip())
            foundFlag2 = 0
        for match in re.finditer(ptr2, line2):
            print("Description:")
            foundFlag2 = 1
            
now = datetime.date.today()
print(now)
with urllib.request.urlopen(url) as response, open("bingBackground" + str(now) + ".jpg", 'wb') as out_file:
    shutil.copyfileobj(response, out_file)
print("Picture saved!")
os.remove("bingXML.txt")
