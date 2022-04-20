// development config
const { merge } = require('webpack-merge');
const webpack = require('webpack');
const commonConfig = require('./common');
const CopyWebpackPlugin = require('copy-webpack-plugin');

module.exports = merge(commonConfig, {
  mode: 'development',
  devtool: 'cheap-module-source-map',
  entry: [
    //"react-hot-loader/patch", // activate HMR for React
    'webpack-dev-server/client?http://localhost:8080', // bundle the client for webpack-dev-server and connect to the provided endpoint
    './index.tsx', // the entry point of our app
    './assets/styles.dev.less'
  ],
  devServer: {
    //hot: "only", // enable HMR on the server
    hot: true,
    historyApiFallback: true, // fixes error 404-ish errors when using react router :see this SO question: https://stackoverflow.com/questions/43209666/react-router-v4-cannot-get-url
    port: 4300
  },
  plugins: [
    new webpack.DefinePlugin({
      'process.env': {
        NODE_ENV: JSON.stringify('development'),
        ENV_CONFIGURATION: JSON.stringify('dev'),
      },
    }),

    new CopyWebpackPlugin({
      patterns: [{ from: './assets/img/**/*' }],
    })
    
  ],
  resolve: {
    // alias: {
    //   'react-dom': '@hot-loader/react-dom'
    // }
  },
  stats: {
    colors: true,
    modules: true,
    reasons: true,
    errorDetails: true,
  },
});
