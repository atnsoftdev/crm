const proxy = require('http-proxy-middleware');

module.exports = function(app) {
  app.use(proxy('/authority', {
    target: 'http://localhost:8080/auth/realms/master',
    changeOrigin: true,
    pathRewrite: {
      '^/authority': ''
    }
  }));
};
