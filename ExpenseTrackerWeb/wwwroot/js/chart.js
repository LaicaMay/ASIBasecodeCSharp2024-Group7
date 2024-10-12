const data = [
    { month: 'January', value: 30, lineValue: 20 },
    { month: 'February', value: 86, lineValue: 50 },
    { month: 'March', value: 168, lineValue: 130 },
    { month: 'April', value: 234, lineValue: 180 },
    { month: 'May', value: 56, lineValue: 90 },
    { month: 'June', value: 97, lineValue: 150 },
    { month: 'July', value: 200, lineValue: 170 },
];

const barSvg = d3.select("#barChart");
const width = +barSvg.attr("width");
const height = +barSvg.attr("height");

const xScale = d3.scaleBand()
    .domain(data.map(d => d.month))
    .range([0, width])
    .padding(0.1);

const yScale = d3.scaleLinear()
    .domain([0, d3.max(data, d => Math.max(d.value, d.lineValue))])
    .range([height, 0]);

// Create bars
barSvg.selectAll(".bar")
    .data(data)
    .enter()
    .append("rect")
    .attr("class", "bar")
    .attr("x", d => xScale(d.month))
    .attr("y", d => yScale(d.value))
    .attr("width", xScale.bandwidth())
    .attr("height", d => height - yScale(d.value));

// Create line
const line = d3.line()
    .x((d, i) => xScale(d.month) + xScale.bandwidth() / 2)
    .y(d => yScale(d.lineValue));

barSvg.append("path")
    .datum(data)
    .attr("class", "line")
    .attr("d", line);

// Create X-axis
const xAxis = d3.axisBottom(xScale);
barSvg.append("g")
    .attr("class", "x-axis")
    .attr("transform", `translate(0, ${height})`) 
    .call(xAxis);

const yAxis = d3.axisLeft(yScale)
    .tickValues(d3.range(0, Math.ceil(d3.max(data, d => Math.max(d.value, d.lineValue))) + 10, 10)); // Set tick values

barSvg.append("g")
    .attr("class", "y-axis")
    .call(yAxis);


//Donut
const donutSvg = d3.select("#donutChart");
const donutWidth = +donutSvg.attr("width");
const donutHeight = +donutSvg.attr("height");
const radius = Math.min(donutWidth, donutHeight) / 2 - 10;

const g = donutSvg.append("g")
    .attr("transform", `translate(${donutWidth / 2}, ${donutHeight / 2})`);

const color = d3.scaleOrdinal(d3.schemeCategory10);

const pie = d3.pie()
    .value(d => d.value);

const arc = d3.arc()
    .innerRadius(radius * 0.5)
    .outerRadius(radius);

const arcs = g.selectAll(".arc")
    .data(pie(data))
    .enter().append("g")
    .attr("class", "arc");

arcs.append("path")
    .attr("d", arc)
    .attr("fill", (d, i) => color(i));

arcs.append("text")
    .attr("transform", d => `translate(${arc.centroid(d)})`)
    .attr("dy", "0.35em")
    .text(d => d.data.month);