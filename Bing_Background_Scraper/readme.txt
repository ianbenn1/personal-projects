Date: January 13th 2018
Language: Python
Author: Ian Bennett
Description:
  This program is a simple Python script for downloading the current background of Bing Canada.
  I found it difficult to manually download the backgrounds from bing, as the links are deeply burried
  in the html. Because of this I decided to write a program to do this for me. The program opens the page
  at https://www.bing.com/HPImageArchive.aspx?format=xml&idx=0&n=1&mkt=en-CA and scrapes it for the <url>
  tag. It then coppies the following line and downloads the picture at that address as a .jpg file labeled
  by date. It currently accesses only the Canadian Bing page, as that is my nationality, but this is
  simply altered by changing the market (mkt) tag in the xml page url. Future editions may prompt the user
  for their choice of locality. This program also currently only downloads the image at the resolution
  specified in the <url> tag. There are however other resolutions available for the images, including 
  (I believe) 1920x1080 versions. In the future I may alter the program to scrape for the <urlBase> tag
  and append a higher, or user prompted resolution.
