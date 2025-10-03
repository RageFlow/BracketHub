
function focus(id) {
    document.getElementById(id).focus();
}

function ScrollToTop() {
    const goToTop = document.getElementById("scroll-to-top");
    if (goToTop) {
        goToTop.scrollIntoView({ behavior: "instant", block: "end" });
    }
}


function highlightMember(tournamentId, memberId) {

    if (tournamentId && memberId && tournamentId > 0 && memberId > 0) {
        var grid = document.getElementById("tournament-bracket-" + tournamentId);

        var members = grid.querySelectorAll(".tournament-" + tournamentId + "-" + memberId);

        members.forEach(highlightElement);
    }
}

function unhighlightMember(tournamentId) {

    if (tournamentId && tournamentId > 0) {
        var grid = document.getElementById("tournament-bracket-" + tournamentId);

        var members = grid.querySelectorAll(".tournament-member");
        console.log(members);

        members.forEach(unhighlightElement);
    }
}

function highlightElement(element, index) {
    setElementHighlight(element, true);
}
function unhighlightElement(element, index) {
    setElementHighlight(element, false);
}

function setElementHighlight(element, bool) {

    if (element) {
        if (bool == true) {
            element.classList.add("highlight");
            var member = element.getAttribute("member-id");

            if (element.parentElement) {
                var att = element.parentElement.getAttribute("match-winner");
                if (att && att == member) {
                    element.parentElement.classList.add("highlight");
                }
            }
        }
        else if (bool == false) {
            element.classList.remove("highlight");

            if (element.parentElement) {
                element.parentElement.classList.remove("highlight");
            }
        }
    }
}