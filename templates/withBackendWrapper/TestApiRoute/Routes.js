// importing modules
const express = require("express");
const router = express.Router();

// importing User Schema

router.get("/health", (req, res) => {
	res.status(200).json("all good");
});

module.exports = router;
