const path = require('path');

const PATHS = {
  entry_file: path.join(__dirname, './src/main.ts'),
  output_dir: path.join(__dirname, '../InColUn/wwwroot/js/')
};


module.exports = {
	resolve: {
    	extensions: ['', '.js', '.ts', '.tsx']
  	},
	entry: PATHS.entry_file,
	output: {
		filename: 'boards.js',
		path: PATHS.output_dir
	},
	externals: {
    	"react": "React",
        "react-dom": "ReactDOM"
	},
  	module: {
    	loaders: [{
      		test: /\.tsx?$/,
      		loader: 'ts-loader',
      		exclude: /node_modules/
   		}]
  	},
	devtool: 'source-map',
	plugins: []
}