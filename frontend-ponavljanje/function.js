var buttonSubmit = document.getElementById("submitButton");
var myForm = document.getElementById("myForm");
var table = document.getElementById("myTable");
var tableBody = document.getElementById("tableBody");
let array = [];
let globalIdx = -1;

populateTable();
disableUpdate();


function onSubmit() {
    let storage = window.localStorage.getItem("Lista");
    console.log(typeof(storage));
    if(storage !== null && storage !== "undefined" && storage !== "") {
        array = JSON.parse(window.localStorage.getItem("Lista"));
    }
    let firstName = document.getElementById("firstName").value;
    let lastName = document.getElementById("lastName").value;
    let bornDate = document.getElementById("bornDate").value;
    
    array.push({firstName, lastName, bornDate});
    window.localStorage.setItem("Lista", JSON.stringify(array));
    populateTable();
}

function populateTable() {
    tableBody.innerHTML = '';
    let storage = window.localStorage.getItem("Lista");
    if(storage !== null && storage !== "undefined" && storage !== "") {
        array = JSON.parse(window.localStorage.getItem("Lista"));
    }
    for(let i = 0; i < array.length; i++) {
        const row = document.createElement("tr");
        const index = i;

        const idxCell = document.createElement("td");
        idxCell.textContent = index;
        
        row.appendChild(idxCell);
        
        Object.values(array[i]).forEach(value => {
            const cell = document.createElement("td");
            cell.textContent = value;
            row.appendChild(cell);
        });

        const actionCell = document.createElement("td");

        const updateButton = document.createElement("button");
        updateButton.type = "button";
        updateButton.textContent = "Update";
        updateButton.addEventListener("click", function() {
            populateUpdate(index);
            globalIdx = index;
        });
        actionCell.appendChild(updateButton);

        const deleteButton = document.createElement("button");
        deleteButton.type = "button";
        deleteButton.textContent = "Delete";
        deleteButton.addEventListener("click", function() {
            array.splice(index, 1);
            window.localStorage.setItem("Lista", JSON.stringify(array));
            populateTable();
        });

        actionCell.appendChild(deleteButton);

        row.appendChild(actionCell);
        

        tableBody.appendChild(row);
    }
}

function disableUpdate() {
    document.getElementById("submitButtonUpdate").value = "";
    document.getElementById("firstNameUpdate").value = "";
    document.getElementById("lastNameUpdate").value = "";
    document.getElementById("bornDateUpdate").value = "";

    document.getElementById("submitButtonUpdate").disabled = true;
    document.getElementById("firstNameUpdate").disabled = true;
    document.getElementById("lastNameUpdate").disabled = true;
    document.getElementById("bornDateUpdate").disabled = true;
}

function populateUpdate(index) {
    document.getElementById("submitButtonUpdate").disabled = false;
    document.getElementById("firstNameUpdate").disabled = false;
    document.getElementById("lastNameUpdate").disabled = false;
    document.getElementById("bornDateUpdate").disabled = false;

    document.getElementById("firstNameUpdate").value = array[index].firstName;
    document.getElementById("lastNameUpdate").value = array[index].lastName;
    document.getElementById("bornDateUpdate").valueAsDate = new Date(array[index].bornDate);
}

function updateData() {
    let firstName = document.getElementById("firstNameUpdate").value;
    let lastName = document.getElementById("lastNameUpdate").value;
    let bornDate = document.getElementById("bornDateUpdate").value;

    array[globalIdx].firstName = firstName;
    array[globalIdx].lastName = lastName;
    array[globalIdx].bornDate = bornDate;
    window.localStorage.setItem("Lista", JSON.stringify(array));
    disableUpdate();
    populateTable();
    globalIdx = -1;
}