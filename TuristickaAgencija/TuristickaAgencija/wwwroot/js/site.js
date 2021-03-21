$(() => {
    let connection = new signalR.HubConnectionBuilder().withUrl("/signalServer").build()

    connection.start()

    connection.on("refreshPutovanja", function () {
        loadData()
    })

    loadData();

    function loadData() {
        var tr = ''

        $.ajax({
            url: '/SignalR/GetPutovanja',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr = tr + `<tr>
                        <td>${v.nazivPutovanja}</td> 
                        <td>${v.opisPutovanja}</td>
                        <td>${v.cijenaPutovanja}</td>
                        <td>${v.datumPolaska}</td>
                        <td>${v.datumDolaska}</td>
                        <td>${v.brojPutnika}</td>
                    </tr>`
                });

                $("#tableBody").html(tr)
            },
            error: (error) => {
                console.log(error)
            }
        })
    }
})