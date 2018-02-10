/*©2018 Greybrick Software c/o IB1*/

document.addEventListener('DOMContentLoaded', function() {
  var checkPageButton = document.getElementById('checkPage');
  checkPageButton.addEventListener('click', function() {


chrome.tabs.getSelected(null, function(tab) {

    var theUrl = tab.url;
    var page;

    var validFlickrUrl = theUrl.search("flickr");
    if(validFlickrUrl == -1)
    {
      window.alert("This extention only works on Flickr.com");
      exit();
    }

    if (window.XMLHttpRequest)
    {// code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp=new XMLHttpRequest();
    }
    else
    {// code for IE6, IE5
        xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
    }
    xmlhttp.onreadystatechange=function()
    {
        if (xmlhttp.readyState==4 && xmlhttp.status==200)
        {
            page = xmlhttp.responseText;
        }
    }
    xmlhttp.open("GET", theUrl, false );
    xmlhttp.send(); 

    
    

    var n = page.indexOf("https://c1.staticflickr.com");

      if(n == -1)
      {
        var n = page.indexOf("https://c2.staticflickr.com");
      }

      
      if(n != -1)
      {
        //window.alert("Found @" + n + " Searching for .jpg");
        var q = page.indexOf(".jpg", n);
        //window.alert(q);

        var picAddress = "";
        var picCounter = 0;
        for(i = n; i <= q + 3; i++)
        {
          picAddress += page[i];
          picCounter++;
        }
        

        var a = document.createElement('a');
        a.href = picAddress;
        a.download = "output.png";
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);

      }
      else
      {
        window.alert("Error. No picture found");
      }

      
      
    });

  

  
    
  }, false);
}, false);

document.addEventListener('DOMContentLoaded', function() {
  var checkPageButton = document.getElementById('instructions');
  checkPageButton.addEventListener('click', function() {


    window.alert("1. Click Download Button in bottom right of picture. \n2. Click view all sizes. \n3. Choose size \n4. Open FlickUR and click Download Picture");

      }, false);
}, false);
