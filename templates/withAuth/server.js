//#region  Imports
const express = require("express");
const path = require("path");
const mongoose = require("mongoose");
const passport = require("passport");
const bodyParser = require("body-parser");
const User = require("./User/Model");
const session = require("express-session");
const LocalStrategy = require("passport-local").Strategy;
const userRoutes = require("./User/Routes");
const secrets = require("./secrets");

const app = express(); //replace with your mongodb connection string.
mongoose.connect("mongodb://localhost:27017/myapp");
//#endregion

//#region  App setup statements
app.use(express.static(path.join(__dirname, "/client/dist")));
app.use(
	require("express-session")({
		secret: secrets.authKey,
		resave: false,
		saveUninitialized: false,
	})
);
app.use(passport.initialize());
app.use(passport.session());
// parse application/x-www-form-urlencoded
app.use(bodyParser.urlencoded({ extended: true }));
// parse application/json
app.use(bodyParser.json());
passport.serializeUser(User.serializeUser());
passport.deserializeUser(User.deserializeUser());
passport.use(new LocalStrategy(User.authenticate()));
app.use("/api/user", userRoutes);
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
