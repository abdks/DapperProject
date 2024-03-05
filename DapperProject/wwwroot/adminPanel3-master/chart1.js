var ctx = document.getElementById('lineChart').getContext('2d');
var myChart = new Chart(ctx, {
    type: 'line',
    data: {
        labels: ['.Istanbul', 'Ankara', 'Bursa','Urfa','Konya','Kastamonu','Karabuk'],
        datasets: [{
            label: 'Earnings in $',
            data: [100, 6000, 2100,1000,3000,2500,1500],
            backgroundColor: [
                'rgba(85,85,85, 1)'

            ],
            borderColor: 'rgb(41, 155, 99)',

            borderWidth: 1
        }]
    },
    options: {
        responsive: true
    }
});