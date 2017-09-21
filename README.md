# MagicDuelsDeckCheck
Utility to determine what cards you are missing from Magic Duels decks posted on the following web sites.

* [Magic Duels Helper](https://www.magicduelshelper.com) 
* [Magic Duels Wiki](http://magicduels.wikia.com/wiki/Decklists)
* [Tapped Out](http://tappedout.net/mtg-decks/search/?q=&format=magic-duels&cards=&general=&price_0=&price_1=&o=-date_updated&submit=Filter+results)
* [Deckstats.Net](https://deckstats.net/decks/search/?lng=en&search_title=&search_format=0&search_price_min=&search_price_max=&search_number_cards_main=&search_number_cards_sideboard=&search_cards%5B%5D=&search_tags=Magic+Duels&search_order=updated%2Cdesc)

## Installation

Download the zip file from [here](https://github.com/Aspallar/MagicDuelsDeckCheck/releases/tag/v1.6.0) unblock the zip (see below) and extract its contents to the folder where you want the application installed.

Run MagicDuelsDeckCheck.exe

or...

Download the DeckCheckSetup.msi file and run it. This will install MagicDuelsDeckCheck and add a shorcut to your start menu.

## Steam profile

The first time you run MagicDuelsDeckCheck you will receive an error message saying that the Magic Duels steam profile couldn't be loaded. Go to File->Options in the menu and specify the path
to your Magic Duels steam profile (there is some help text in the options dialog to help you locate this). The steam profile contains a local copy of what cards you own and is only read and not altered in any way by MagicDuelsDeckCheck.


## Use

To use MagicDuelsDeckHelper just drag a deck link from the one of the supported web sites and drop it in the applications window. A page will be displayed in your browser listing the cards that you are missing, if any, and what set they can be found in.

### Deck links

If you are on a page that lists decks, just drag the link for the deck you are interested in.

![Drag Example Image](https://github.com/Aspallar/MagicDuelsDeckCheck/blob/master/images/draglink.png)

If you are on the actual deck page, drag the address bar in your browser.

![Drag Example Image](https://github.com/Aspallar/MagicDuelsDeckCheck/blob/master/images/dragaddress.png)

## Browsers

This initial release has only been tested in Chrome, it should work in other browsers though.

If you are using Internet Explorer you will probably get a "blocked content" warning when the results page is displayed, you should allow blocked content for the page.

## Unblocking downloaded zip

Before extracting the contents of the installation zip file you should unblock the zip file, otherwise the extracted files will be marked as blocked and you will get a warning message every time you run it.

To unblock the zip right click on it and choose properties, check the 'Unblock' check box and click on ok or click on the 'Unblock' button, depending on which version of windows you are using.

