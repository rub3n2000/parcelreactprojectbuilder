//#region  Imports
const express = require("express");
const path = require("path");
const bodyParser = require("body-parser");

const testRoutes = require("./TestApiRoute/Routes");

const app = express();
//#endregion

//#region  App setup statements
app.use(express.static(path.join(__dirname, "/client/dist")));

app.use(bodyParser.urlencoded({ extended: true }));
// parse application/json
app.use("/api/test", testRoutes);
//#endregion

//#region Client Rendering Route
app.get("*", (req, res) => {
	res.sendFile("./index.html", {
		root: path.join(__dirname, "/client/dist"),
	});
});
//#endregion

//#region Server Starting
const port = process.env.PORT || 5000;

app.listen(port, () => console.log(`Server started on port ${port}`));
//#endregion
