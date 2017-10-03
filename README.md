# MagicDuelsDeckCheck
Utility to determine what cards you are missing from Magic Duels decks posted on the following web sites.

* [Magic Duels Helper](https://www.magicduelshelper.com) 
* [Magic Duels Wiki](http://magicduels.wikia.com/wiki/Decklists)
* [Tapped Out](http://tappedout.net/mtg-decks/search/?q=&format=magic-duels&cards=&general=&price_0=&price_1=&o=-date_updated&submit=Filter+results)
* [Deckstats.Net](https://deckstats.net/decks/search/?lng=en&search_title=&search_format=0&search_price_min=&search_price_max=&search_number_cards_main=&search_number_cards_sideboard=&search_cards%5B%5D=&search_tags=Magic+Duels&search_order=updated%2Cdesc)

MagicDuelsDeckCheck can also check decks defined locally in .dec or .txt files.

## Installation

Download the DeckCheckSetup.msi file from [here](https://github.com/Aspallar/MagicDuelsDeckCheck/releases/tag/v1.7.0) and run it. This will install MagicDuelsDeckCheck and add a shortcut to your start menu.

*Or...*

Download the zip file  unblock the zip (see below) and extract its contents to the folder where you want the application installed.

Run MagicDuelsDeckCheck.exe

*Or...*

Download the source and compile it.

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

You can also check decks defined in local text file (.txt or .dec files) by dragging them from windows explorer into the MagicDuelsDeckCheck window.

## Browsers

This initial release has only been tested in Chrome, it should work in other browsers though.

If you are using Internet Explorer you will probably get a "blocked content" warning when the results page is displayed, you should allow blocked content for the page.

## Unblocking downloaded zip

Before extracting the contents of the installation zip file you should unblock the zip file, otherwise the extracted files will be marked as blocked and you will get a warning message every time you run it.

To unblock the zip right click on it and choose properties, check the 'Unblock' check box and click on ok or click on the 'Unblock' button, depending on which version of windows you are using.

## Text file format

MagicDuelsDeckCheck uses the following rules when reading decks from text files (.dec or .txt).

All lines in the file that do not start with a number are ignored.

Everything that comes after the word 'sideboard' is ignored.

All lines that start with a number are assumed to be cards. They should each be in the format

4x Card Name

The 'x' is optional and may be preceded by spaces. There must be at least one space after the 'x' if it is included. The '4' is the number of cards.

The following is an example the contents of a valid text file deck definition.

```text
Blue: 15
(arbitrary comments  like the above and this won't be read
as the line does not start with a number)
4x Frost Lynx
3x Whirler Rogue
4x Countermand
1x Disciple of the Ring
2x Willbreaker

Green: 18
4 x Fog
4 x Might of the Masses
4 x Timberland Guide
2 x Evolutionary Leap
3 x Gather the Pack
1 x Nissa, Vastwood Seer

Multi-Color: 3
3 x Bounding Krasis

Land: 24
8 Island
3 Forest
2 Hinterland Harbor
3 Rogue's Passage
4 Simic Guildgate
4 Evolving Wilds
```

Entries like the following are not valid

```
4Frost Lynx
3xWhirler Rogue
```
as there is no space after the 'x' or the number.
