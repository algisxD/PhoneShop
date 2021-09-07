function vw() {
    return window.innerWidth;
}

function elementBounding(elementId, side) {
    var element = document.getElementById(elementId);
    var rectangle = element.getBoundingClientRect();
    if (side == "top") {
        return rectangle.top;
    }
    else if (side == "right") {
        return rectangle.right;
    }
    else if (side == "bottom") {
        return rectangle.bottom;
    }
    else if (side == "left") {
        return rectangle.left;
    }
    else {
        return -1;
    }
}


function ScrollIntoView(elementId) {
    var element = document.getElementById(elementId);
    if (!element) {
        console.warn('element was not found', elementId);
        return false;
    }
    //element.scrollIntoViewIfNeeded({ behavior: 'smooth' });
    //element.scrollIntoView({ behavior: "smooth", block: "center" });
    return true;
}

function ScrollIntoViewFixed(elementId, transition, block) {
    var element = document.getElementById(elementId);
    //element.scrollIntoView({ behavior: transition, block: block });
    //element.scrollIntoViewIfNeeded({ behavior: 'smooth' });
    element.scrollIntoView({ behavior: transition, block: 'nearest', inline: 'start' })
    return true;
}


function ScrollBy(elementId, amount) {
    document.getElementById(elementId).scrollBy(0, amount);




    //if (amount > 0) {
    //    document.getElementById(elementId).scrollBy({
    //        top: amount,
    //        behavior: 'smooth'
    //    });
    //}
    //else {
    //    document.getElementById(elementId).scrollBy({
    //        bottom: amount,
    //        behavior: 'smooth'
    //    });
    //}
}

function ScrollTop(elementId) {
    return document.getElementById(elementId).scrollTop;
}

function OnScroll(elementId) {
    var element = document.getElementById(elementId);
    var lastScrollTop = 0;

    // element should be replaced with the actual target element on which you have applied scroll, use window in case of no target element.
    element.addEventListener("scroll", function () { // or window.addEventListener("scroll"....
        var st = window.pageYOffset || document.documentElement.scrollTop; // Credits: "https://github.com/qeremy/so/blob/master/so.dom.js#L426"
        if (st > lastScrollTop) {
            // downscroll code
        } else {
            // upscroll code
        }
        lastScrollTop = st <= 0 ? 0 : st; // For Mobile or negative scrolling
    }, false);
}


function SetFocus(elementId) {
    document.getElementById(elementId).focus();
}

function SetAttributeByClass(className, attribute, value) {
    var elements = document.getElementsByClassName(className);
    for (let i = 0; i < elements.length; i++) {
        elements[i].setAttribute(attribute, value);
    }
}




function overrideDefaultScrollKeys(e) {
    switch (e.keyCode) {
        case 37: case 39: case 38: case 40: // Arrow keys
        case 32: e.preventDefault(); break; // Space
        default: break; // do not block other keys
    }
}


function EnableDefaultScrollKeysOverride() {
    document.body.addEventListener("keydown", overrideDefaultScrollKeys, false);
}

function DisableDefaultScrollKeysOverride() {
    document.body.removeEventListener("keydown", overrideDefaultScrollKeys, false);
}