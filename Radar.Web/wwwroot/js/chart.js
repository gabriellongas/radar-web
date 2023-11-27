// Get the HTML canvas by its id 
plots = document.getElementById("chart");
// Example datasets for X and Y-axes 
const xValues = ['00:00', '01:00', '02:00', '03:00', '04:00', '05:00', '06:00', '07:00', '08:00', '09:00', '10:00', '11:00', '12:00', '13:00', '14:00', '15:00', '16:00', '17:00', '18:00', '19:00', '20:00', '21:00', '22:00', '23:00'];
const yValues = [20, 22, 21, 21, 22, 23, 44, 55, 53, 51, 58, 76, 84, 91, 75, 62, 51, 63, 72, 88, 93, 95, 73, 51, 33];
// Create an instance of Chart object:
new Chart(plots, {
    type: 'line', //Declare the chart type 
    data: {
        labels: xValues, //X-axis data 
        datasets: [{
            data: yValues, //Y-axis data 
            backgroundColor: '#067BC2',
            borderColor: '#84bcda',
            fill: false
        }]
    },
    options: {
        maintainAspectRatio: false,
        plugins: {
            legend: {
                display: false
            },
            title: {
                display: true,
                text: 'Mapa de ruído',
                position: 'top',
                align: 'start',
                color: '#000000',
                padding: 15,
                font: {
                    size: 24,
                    family: 'Segoe UI',
                    weight: 500
                }
            }
        },
        scales: {
            y: {
                suggestedMin: 0,
                suggestedMax: 100
            }
        }
    }
});