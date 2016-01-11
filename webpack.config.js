const DEBUG = true;

var webpack = require('webpack');

module.exports = {
    entry: './src/index',
    output: {
        path: __dirname,
        publicPath: "/",
        filename: "./src/app.js"
    },
    watch: DEBUG,
    devtool: DEBUG ? "cheap-module-inline-source-map" : null,
    module: {
        loaders: [
            {
                test: /\.jsx?$/,
                exclude: /(node_modules|bower_components)/,
                loader: 'babel',
                query: {
                    presets: ['react', 'es2015']
                }
            },
            {
                test: /\.less$/,
                exclude: /(node_modules|bower_components)/,
                loader: 'style!css!less'
            },
            { test: /\.woff(\?v=\d+\.\d+\.\d+)?$/,   loader: "url?limit=10000&mimetype=application/font-woff&name=src/assets/[name].[ext]" },
            { test: /\.woff2(\?v=\d+\.\d+\.\d+)?$/,   loader: "url?limit=10000&mimetype=application/font-woff&name=src/assets/[name].[ext]" },
            { test: /\.ttf(\?v=\d+\.\d+\.\d+)?$/,    loader: "url?limit=10000&mimetype=application/octet-stream&name=src/assets/[name].[ext]" },
            { test: /\.eot(\?v=\d+\.\d+\.\d+)?$/,    loader: "file?name=src/assets/[name].[ext]" },
            { test: /\.svg(\?v=\d+\.\d+\.\d+)?$/,    loader: "url?limit=10000&mimetype=image/svg+xml&name=src/assets/[name].[ext]" },
            {
                test: /\.(png|jpg)$/,
                loader: "file?name=src/assets/[name].[ext]"
            }
        ]
    },
    plugins: []
};

if(!DEBUG){
    module.exports.plugins.push(
        new webpack.optimize.UglifyJsPlugin({
            compress:{
                warnings: false,
                drop_console: true,
                unsafe: true
            }
        })
    );
}

