import webpack from 'webpack';


function format(time) {
    return time.toTimeString().replace(/.*(\d{2}:\d{2}:\d{2}).*/, '$1');
}

var task = (name, fn) => async () => {
    const start = new Date();
    console.log(`[${format(start)}] Starting '${name}'...`);
    await fn();
    const end = new Date();
    const time = end.getTime() - start.getTime();
    console.log(`[${format(end)}] Finished '${name}' after ${time} ms`);
};


task('release', async () => new Promise((resolve, reject) => {
    process.argv.push('release');
    const config = require('../webpack.config');
    const bundler = webpack(config);

    function bundle(err) {
        if (err) {
            return reject(err);
        }
        return resolve();
    }
    bundler.run(bundle);
}))().catch(err => console.error(err.stack));