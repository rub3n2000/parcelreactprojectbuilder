#!/usr/bin/env node

const inquirer = require("inquirer");
const fs = require("fs");
const ncp = require("ncp");

const CURR_DIR = process.cwd();
const CHOICES = fs.readdirSync(`${__dirname}/templates`);

const QUESTIONS = [
	{
		name: "project-choice",
		type: "list",
		message: "What project template would you like to generate?",
		choices: CHOICES,
	},
	{
		name: "project-name",
		type: "input",
		message: "Project name:",
		validate: function (input) {
			if (/^([A-Za-z\-\_\d])+$/.test(input)) return true;
			else
				return "Project name may only include letters, numbers, underscores and hashes.";
		},
	},
];

inquirer.prompt(QUESTIONS).then((answers) => {
	const projectChoice = answers["project-choice"];
	const templatePath = `${__dirname}/templates/${projectChoice}`;
	const projectName = answers["project-name"];
	const targetFolderPath = `${CURR_DIR}/${projectName}`;
	ncp(templatePath, targetFolderPath, (err) => {
		if (err) return console.error(err);
		console.log("done");
	});
});
