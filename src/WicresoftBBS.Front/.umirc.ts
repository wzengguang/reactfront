export default {
  npmClient: 'cnpm',
  dva: {},
  antd: {},
  access: {},
  model: {},
  initialState: {},
  request: {},
  routes: [
    { path: '/', component: 'index' },
    { path: '/login', component: 'login/login' },
    { path: '/posts', component: 'posts/list/list' },
    { path: '/posts/detail/:postId', component: 'posts/detail/detail' },
  ],
  proxy: {
    '/api': {
      target: 'http://localhost:7042/',
      changeOrigin: true,
      pathRewrite: { '^/api': '/api' },
    },
  }
};
