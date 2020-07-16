const User = require("./User/Model");

module.exports = {
  isLoggedIn: function (req, res, next) {
    if (req.isAuthenticated()) {
      return next();
    } else {
      res.redirect("/");
    }
  },
  isAdmin: function (req, res, next) {
    if (req.user && req.isAuthenticated()) {
      User.findById(req.user._id, function (err, user) {
        if (err) {
          res.status(500).send(err);
        } else {
          if (user.isAdmin) {
            return next();
          }
          res.status(401).send(null);
        }
      });
    } else {
      res.status(401).send(null);
    }
  },
};
