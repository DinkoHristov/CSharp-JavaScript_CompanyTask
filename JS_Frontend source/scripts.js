//  Usefull links :
//  http://xpather.com/
//  https://developer.mozilla.org/en-US/docs/Web/API/XPathResult
function download_csv(csv, filename) {
    var csvFile;
    var downloadLink;

    // CSV FILE
    csvFile = new Blob([csv], {type: "text/csv"});

    // Download link
    downloadLink = document.createElement("a");

    // File name
    downloadLink.download = filename;

    // We have to create a link to the file
    downloadLink.href = window.URL.createObjectURL(csvFile);

    // Make sure that the link is not displayed
    downloadLink.style.display = "none";

    // Add the link to your DOM
    document.body.appendChild(downloadLink);

    downloadLink.click();
}

function export_table_to_csv(filename) {
	var csv = [];
	var rows = document.querySelectorAll("table tr");
	
    for (var i = 0; i < rows.length; i++) {
		var row = [], cols = rows[i].querySelectorAll("td, th");
		
        for (var j = 0; j < cols.length; j++) 
            row.push(cols[j].innerText);
        
		csv.push(row.join(","));		
	}

    // Download CSV
    download_csv(csv.join("\n"), filename);
}

function generateTable() {
    // Fetch XML file and generate table dynamically
    // fetch('https://test.ce2s.net/Study.xml')
    fetch('Study.xml')
        .then(response => response.text())
        .then(xmlString => {
            // Parse XML
            var parser = new DOMParser();
            var xmlDoc = parser.parseFromString(xmlString, "text/xml");

            // Extract data using XPath
            var aliases = xmlDoc.evaluate('//alias', xmlDoc, null, XPathResult.ANY_TYPE, null);
            var fields = xmlDoc.evaluate('//field', xmlDoc, null, XPathResult.ANY_TYPE, null);

            // Create table dynamically
            var table = document.createElement('table');
            
            // Add headers to the table
            var headerRow = table.insertRow();
            var alias = aliases.iterateNext();
            while (alias) {
                var headerCell = headerRow.insertCell();
                headerCell.textContent = alias.textContent;
                alias = aliases.iterateNext();
            }

            // Add data rows to the table
            while (fields.iterateNext()) {
                var row = table.insertRow();
                fields = xmlDoc.evaluate('//field', xmlDoc, null, XPathResult.ANY_TYPE, null);

                var field = fields.iterateNext();
                while (field) {
                    var cells = row.insertCell();
                    cells.textContent = field.textContent;
                    field = fields.iterateNext();
                }
            }

            // Add the table to the document body
            document.body.appendChild(table);
        })
        .catch(error => console.error('Error fetching XML:', error));
}