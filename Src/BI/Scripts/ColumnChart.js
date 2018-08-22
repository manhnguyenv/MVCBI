$(document).ready(function () {
  //Load Data Here  
  var chartData = null;
  $.ajax({
    url: '/Admin/GetDataColumnChartByQuarter',
    type: 'GET',
    dataType: 'json',
    data: '',
    success: function (d) {
      chartData = d;
    },
    error: function () {
      alert('Error!');
    }
  }).done(function () {
    drawChart(chartData);
  });
});
function drawChart(d) {
  var chartData = d;
  var data = null;
  data = google.visualization.arrayToDataTable(chartData);

  var view = new google.visualization.DataView(data);
  view.setColumns([0,
    {
      type: 'number',
      label: data.getColumnLabel(0),
      calc: function () { return 0; }
    }, {
      type: 'number',
      label: data.getColumnLabel(1),
      calc: function () { return 0; }
    }, {
      type: 'number',
      label: data.getColumnLabel(2),
      calc: function () { return 0; }
    }, {
      type: 'number',
      label: data.getColumnLabel(3),
      calc: function () { return 0; }
    }, {
      type: 'number',
      label: data.getColumnLabel(4),
      calc: function () { return 0; }
    }]);
  var columnChart = new google.visualization.ColumnChart(document.getElementById('visualization'));
  var options = {
    title: 'Doanh thu theo quý',
    legend: 'bottom',
    hAxis: {
      title: 'Year',
      format: '#'
    },
    vAxis: {
      minValue: 0,
      maxValue: 1000,
      title: 'Doanh thu theo quý'
    },
    chartArea: {
      left: 100, top: 50, width: '70%', height: '50%'
    },
    animation: {
      duration: 1000
    }
  };

  var runFirstTime = google.visualization.events.addListener(columnChart, 'ready', function () {
    google.visualization.events.removeListener(runFirstTime);
    columnChart.draw(data, options);
  });

  columnChart.draw(view, options);

  var dataPieChart = new google.visualization.DataTable();
  dataPieChart.addColumn('string', 'Tháng');
  dataPieChart.addColumn('number', 'Lợi nhuận');
  dataPieChart.addRows([
    ['Tháng 7', { v: 30, f: '30%' }],
    ['Tháng 8', { v: 35, f: '35%' }],
    ['Tháng 9', { v: 35, f: '35%' }]
  ]);

  var pieChartOption = {
    title: 'Lợi nhuận theo quý',
    width: 500,
    height: 400,
    is3D: true
  };

  var pieChart = new google.visualization.PieChart(document.getElementById('3DPieChart'));
  pieChart.draw(dataPieChart, pieChartOption);
}
google.load('visualization', '1', { packages: ['corechart'] });  
