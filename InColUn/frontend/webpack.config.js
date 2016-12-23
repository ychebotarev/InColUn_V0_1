const path = require('path');

const PATHS = {
  entry_main: path.join(__dirname, './src/main.ts'),
  entry_login: path.join(__dirname, './src/login.ts'),
  output_dir: path.join(__dirname, '../backend/src/InColUn/wwwroot/js/')
};


module.exports = {
	resolve: {
    	extensions: ['', '.js', '.ts', '.tsx']
  	},
	entry: 
	{ 
		main:PATHS.entry_main, 
		login:PATHS.entry_login 
	},
	output: {
		filename: '[name].js',
		path: PATHS.output_dir
	},
	externals: {
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