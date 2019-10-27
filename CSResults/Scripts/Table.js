window.onload = function () {
    document.getElementById('resultsSearch').addEventListener('keyup', filterTable)
};

//Filters table based on text in searchbox
function filterTable() {

    var input, filter, table, td, i;

    input = document.getElementById("resultsSearch");
    filter = input.value.toUpperCase();
    table = document.getElementById("resultsTable");


    for (i = 0; i < table.rows.length; i++) {

        //Gets the text of the inner rows  
        td = table.rows[i].innerText;

        if (td) {
            if (td.toUpperCase().indexOf(filter) > -1) {

                //Show the row if there is a match
                table.rows[i].style.display = "";

            } else if (table.rows[i].id == "body") {

                /*Do not display the current row if there is no match 
                 * and it is not a header row
                 */
                table.rows[i].style.display = "none";

            }
        }
    }
}
