﻿class Deck {
    constructor() {
        this.cards = [];
    }
}

var deckHandler = {
    set addCard(div) {
        this.deck.appendChild(div);
    },
    get allCards() {
        var d = new Deck();
        this.deck.childNodes.forEach(
            function (t = this) {
                if (t !== null) {
                    d.cards.push(t);
                }
            }
        );
        return d;
    },
    deck: []
};
console.log(chrome.windows, chrome.tabs);
chrome.windows.getAll({ populate: true }, getAllOpenWindows);
function getAllOpenWindows(winData) {
    var tabs = [];
    for (var i in winData) {
        if (winData[i].focused === true) {
            var winTabs = winData[i].tabs;
            var totTabs = winTabs.length;
            for (var j = 0; j < totTabs; j++) {
                tabs.push(winTabs[j].url);
            }
        }
    }
    console.log(tabs);
}

var card = [];
var counter = 0;
deckHandler.deck = document.getElementById("cardHolder");
for (var i = 0; i < localStorage.length; i++) {
    if (localStorage.getItem(localStorage.key(i)).split("1") !== undefined) {
        AddCard(document.getElementsByClassName(localStorage.getItem(localStorage.key(i)).split("1", 1)[0]).item(0).cloneNode(true));
    } else {
        AddCard(document.getElementsByClassName(localStorage.getItem(localStorage.key(i))).item(0).cloneNode(true));
    }
}
function cardClassHanldere(age) {
    counter = 0;
    var someting;
    var divs = Array.from(document.querySelectorAll("#cardHolder > .cards"), element => element.className.split(" ", 1)[0]);
    if (age.className !== undefined) {
        if (divs.length >= 30) {
            return false;
        }
        card = divs;
        var tempcard = [];
        for (someting of card) {
            tempcard.push(someting);
        }
        for (var currcard of tempcard) {
            if (currcard === age.className.split(" ", 1)[0]) {
                counter++;
                if (age.dataset.rarety.toLowerCase() === "legendary") {
                    if (counter >= 1) {
                        return false;
                    }
                }
                if (counter >= 2) {
                    return false;
                }
            }
        }
    }
    return true;
}
//function getCookie(name) {
//    var nameEQ = name + "=";
//    var ca = document.cookie.split(';');
//    for (var i = 0; i < ca.length; i++) {
//        var c = ca[i];
//        while (c.charAt(0) === ' ') c = c.substring(1, c.length);
//        if (c.indexOf(nameEQ) === 0) return c.substring(nameEQ.length, c.length);
//    }
//    return null;
//}
function AddCard(inp) {
    if ($(inp).parent("#cardHolder").hasClass("cardHolder")) {
        inp.remove();
        if (localStorage.getItem(inp.className.split("cards", 1)[0] + "1") !== null) {
            localStorage.removeItem(inp.className.split("cards", 1)[0] + "1");
        } else {
            localStorage.removeItem(inp.className.split("cards", 1)[0]);
        }
    } else {
        ages = deckHandler.allCards.cards;
        if (cardClassHanldere(inp) === true) {
            deckHandler.deck = document.getElementById("cardHolder");
            deckHandler.addCard = inp.cloneNode(true);
            ages = deckHandler.allCards.cards;
            if (localStorage.getItem(inp.className.split("cards", 1)[0]) !== null) {
                localStorage.setItem(inp.className.split("cards", 1)[0] + "1", inp.className.split("cards", 1)[0]);
            } else {
                localStorage.setItem(inp.className.split("cards", 1)[0], inp.className.split("cards", 1)[0]);
            }
        }
        else {
            alert("you can only have 2 of each card in your deck and your deck can only have 30 cards");
        }
    }
}

function HeroSelector(Hero) {
    $(document).ready(function () {
        let result = null;
        let scriptUrl = "Decks?Hero=" + Hero;
        $.ajax({
            url: scriptUrl,
            method: "GET",
            type: "get",
            dataType: 'html',
            async: false,
            success: function (data) {
                result = data;
            }
        });
        document.cookie = "hero=" + Hero;
        $("html").html(result);
    });
}
function saveDeck() {
    $(document).ready(function () {

        let result = null;
        let scriptUrl = "Decks/Create";
        let content = [];
        if ($("#cardHolder > .cards").length === 30) {
            for (let i = 0; i < $("#cardHolder > .cards").length; i++) {
                content.push($("#cardHolder > .cards")[i].getAttribute("data-id"));
            }
            $.post({
                url: scriptUrl,
                dataType: { CardInfoesId: content },
                success: function (data) { result = data; }
            });
            console.log(result);
        }
        else {
            alert("you need 30 cards in your deck");
        }
    });
}





