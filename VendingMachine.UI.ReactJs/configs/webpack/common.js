// shared config (dev and prod)
const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
  resolve: {
    // alias: {
    //   src: path.resolve(__dirname, 'src/'),
    // },
    extensions: ['.ts', '.tsx', '.js', '.jsx'],
  },
  context: path.resolve(__dirname, '../../src'),
  module: {
    rules: [
      // {
      //   test: /\.(js|jsx|ts|tsx)$/,
      //   exclude: /node_modules/,
      //   //include: path.join(__dirname, 'src'),
      //   //use: ['babel-loader', 'awesome-typescript-loader']
      //   //use: ['react-hot-loader/webpack', 'awesome-typescript-loader']
      //   use: [
      //     'react-hot-loader/webpack',
      //     {
      //       loader: 'babel-loader',
      //       // options: {
      //       //   transpileOnly: true,
      //       //   experimentalWatchApi: true,
      //       // },
      //     },
      //   ],
      // },
      {
        test: /\.(ts|js)x?$/,
        use: ["babel-loader"],
        exclude: /node_modules/,
      },
      {
        test: /\.css$/,
        use: ['style-loader', 'css-loader'],
      },
      {
        test: /\.less$/,
        use: ['style-loader', 'css-loader', 'less-loader']
      },
      // {
      //   test: /\.(scss|sass)$/,
      //   use: ['style-loader', 'css-loader', 'sass-loader'],
      // },
      {
        test: /\.(jpe?g|png|gif|svg)$/i,
        include: [path.join(__dirname, 'assets/img')],
        loader: 'file-loader',
        options: {
            name: '[name].[ext]',
            outputPath: 'images'
        }
        // use: [
        //   'file-loader?hash=sha512&digest=hex&name=img/[contenthash].[ext]',
        //   'image-webpack-loader?bypassOnDebug&optipng.optimizationLevel=7&gifsicle.interlaced=false',
        // ],
      },
    ],
  },
  plugins: [new HtmlWebpackPlugin({ template: '../html/index.html.ejs' })],
  externals: {
    react: 'React',
    'react-dom': 'ReactDOM',
  },
  performance: {
    hints: false,
  },
};
