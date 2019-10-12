let connection = null;
setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/countHub")//默认协议按照从最高到最低降级,请求后台CountHub类，驼峰形式
        //.withUrl("/countHub", signalR.HttpTransportType.LongPolling)//指定用longpolling协议
        .build();

    connection.on("ReceiveUpdate",
        (update) => {
            const resultDiv = document.getElementById("result");
            resultDiv.innerHTML = update;
        });
    connection.on("someFunc",
        function(obj)  {
            const resultDiv = document.getElementById("result");
            resultDiv.innerHTML ="Someone called,parameters="+ojb.random;
        });
    connection.on("finished",
        function () {
            connection.stop();
            const resultDiv = document.getElementById("result");
            resultDiv.innerHTML = "finished";
        });
    connection.start().catch(err => console.error(err.toString()));
};
setupConnection();

document.getElementById("submit").addEventListener("click", e => {
    e.preventDefault();
    fetch("/api/count",
        {
            method: "POST",
            headers: {
                "content-type": "application/json"
            }
        }).then(response => response.text()).then(id => connection.invoke("GetLatestCount", id));

});