// shared config (dev and prod)
const path = require('path');

module.exports = {
  output: {
    filename: 'bundle.js',
    path: path.resolve(__dirname, '../dist/'),
  },
  resolve: {
    // пути должны быть от текущего файла!
    alias: {
      app: path.resolve(__dirname, '../../src/app.tsx'),
      src: path.resolve(__dirname, '../../src/')
    },
    extensions: ['.ts', '.tsx', '.js', '.jsx', '.json'],
  },
  context: path.resolve(__dirname, '../../src'),
  module: {
    rules: [
      {
        test: /\.(js|jsx|tsx|ts)$/,
        exclude: /node_modules/,
        use: [
          {
            loader: 'ts-loader',
            options: {
              transpileOnly: true,
            }
          }
        ],
      },
      {
        test: /\.css$/,
        use: ['style-loader', 'css-loader'],
      },
      {
        test: /\.less$/,
        use: ['style-loader', 'css-loader', 'less-loader'],
      },
      // {
      //   test: /\.(scss|sass)$/,
      //   use: ['style-loader', 'css-loader', 'sass-loader'],
      // },
      {
        test: /\.(jpe?g|png|gif|svg)$/i,
        include: [path.join(__dirname, './assets/img')],
        loader: 'file-loader',
        options: {
          name: '[name].[ext]',
          outputPath: 'images',
        }
      },
    ],
  },
  plugins: [],
  externals: {
    react: 'React',
    'react-dom': 'ReactDOM',
  },
  performance: {
    hints: false,
  }
};
