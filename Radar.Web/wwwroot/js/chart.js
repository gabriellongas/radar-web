// Get the HTML canvas by its id 
plots = document.getElementById("plots");
// Example datasets for X and Y-axes 
const xValues = ['00:00', '01:00', '02:00', '03:00', '04:00', '05:00', '06:00', '07:00', '08:00', '09:00', '10:00', '11:00', '12:00', '13:00', '14:00', '15:00', '16:00', '17:00', '18:00', '19:00', '20:00', '21:00', '22:00', '23:00'];
const yValues = [-100, -100,-80 , -80, -80, -20, 0, 10, 14, 15, 25,10,30, 40,55, 68, 76,82,100,120,150, 160,180,120,100];
// Create an instance of Chart object:
new Chart(plots, {
    type: 'line', //Declare the chart type 
    data: {
        labels: xValues, //X-axis data 
        datasets: [{
            data: yValues, //Y-axis data 
            backgroundColor: '#067BC2',
            borderColor: '#84bcda',
            fill: false, //Fills the curve under the line with the babckground color. It's true by default 
        }]
    },
});