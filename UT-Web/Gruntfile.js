module.exports = function(grunt) {

  // Project configuration.
  grunt.initConfig({
    pkg: grunt.file.readJSON('package.json'),
    compass: {
    	dist: {
    		options: {
    			sassDir: 'scss',
    			cssDir: 'css'
    		}
    	}
    }
  });

  grunt.loadNpmTasks('grunt-contrib-compass');
};