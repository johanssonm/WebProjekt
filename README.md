### Cleancode

    ###**NewsController.cs**
      #TODO: Döp om route. I.g. ta bort news från alla undersidor
      #TODO: Ta bort anonym typ
     #TODO: Märk upp meddelande för modelstate
      #TODO: Flytta till ett interface
      #TODO: Flytta till ett interface
      #TODO: Ta bort anonym typ
      #TODO: Flytta till ett interface
      #TODO: Hitta på ett bättre namn(?)
      #TODO: Kanske flytta till en testdata-behållare
      #TODO: Kanske flytta till en testdata-behållare
      #TODO: Kanske flytta till en testdata-behållare
      #TODO: Kanske flytta till db-services
  wwwroot
    public
      assets
        js
          dropzone.js
            bug in Internet Explorer 11 (SCRIPT65535 exception)
            bug in latest Chrome versions.
            bug.
            bug-ios
            bug which squash image vertically while drawing into canvas for some images.
            bug in iOS6 devices. This function from https://github.com/stomita/ios-imagefile-megapixel
        vendor
          bootstrap
            js
              bootstrap.bundle.js
                todo (fat): these should probably be refactored out of modal.js
                TODO (fat): remove sketch reliance on jQuery position/offset
              bootstrap.js
                todo (fat): these should probably be refactored out of modal.js
                TODO (fat): remove sketch reliance on jQuery position/offset
          dynatable
            jquery.dynatable.js
              TODO: figure out a better way to do this.
              TODO: Wrap this in a try/rescue block to hide the processing indicator and indicate something went wrong if error
              TODO: automatically convert common types, such as arrays and objects, to string
          jquery
            jquery.js
              TODO: identify versions
              TODO: identify versions
              bug in IE8/9 that throws an error
              bug restricted)
              TODO: Drop _data, _removeData)
              TODO: Now that all calls to _data and _removeData have been replaced
              bug (it existed in older Chrome versions as well).
              bug #9237
              bug won't allow us to use `1 - ( 0.5 || 0 )` (#12497)
              bug: https://bugs.webkit.org/show_bug.cgi?id=29084
              bug: https://bugs.chromium.org/p/chromium/issues/detail?id=589347
            jquery.slim.js
              TODO: identify versions
              TODO: identify versions
              bug in IE8/9 that throws an error
              bug restricted)
              TODO: Drop _data, _removeData)
              TODO: Now that all calls to _data and _removeData have been replaced
              bug (it existed in older Chrome versions as well).
              bug #9237
              bug: https://bugs.webkit.org/show_bug.cgi?id=29084
              bug: https://bugs.chromium.org/p/chromium/issues/detail?id=589347


# WebProjekt



    Sök och hämta nyheter från nätet. Anslut mot nyhetsbyråer. RSS-flöde.

#    Gui: nyhetssidan som blogg. Visa för varje artikel. Varje artikel har en sida. Bild. Författare.

    Kunna skriva ut en artikel

    Formattera text (typsnitt, fetstil)

OK! Lägga in taggar och söka på dem. Metadata. => Vi gjorde kategorier istället

    Författare för artikel

#    Mobilanpassad sida.

    Söka i sina egna nyheter

#   "See more" för långa brödtexter
    

#    Välja hur artiklarna ska sorteras.

    Allmän sök: först taggar, sedan brödtext.

    Eventsourcing på databasen. Se utvecklingen på artikeln.

    Geotaggning. Få nyheter där du är.
    
   # Väderwidget, typ väder med hjälp av ort via Ip
    
    Posta på FB, Likea
    
