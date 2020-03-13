#As seen on #https://medium.com/@avatsaev/create-efficient-angular-docker-images-with-multi-stage-builds-907e2be3008d
### STAGE 1: Build ###

FROM node:13.3.0 as builder
COPY package.json package-lock.json ./

## Storing node modules on a separate layer will prevent unnecessary npm installs at each build

RUN npm i && mkdir /ng-app && cp -R ./node_modules ./ng-app
WORKDIR /ng-app
COPY . .

## Build the angular app in production mode and store the artifacts in dist folder
RUN $(npm bin)/ng build --prod

### STAGE 2: Setup ###
FROM nginx:alpine

RUN chmod -R o+rX /usr/share/nginx/html
RUN chown nginx:nginx /usr/share/nginx/html/*

## Remove default nginx website
RUN rm -rf /usr/share/nginx/html/*

## Copy our default nginx config
COPY nginx.conf /etc/nginx/conf.d/default.conf

## From ‘builder’ stage copy over the artifacts in dist folder to default nginx public folder
COPY --from=builder /ng-app/dist /usr/share/nginx/html

#CMD ["nginx", "-g", "daemon off;"]

##########

### Remove default nginx website
#RUN rm -rf /usr/share/nginx/html/*
## From 'builder' stage copy over the artifacts in dist folder to default nginx public folder
#COPY /dist /usr/share/nginx/html
#CMD ["nginx", "-g", "daemon off;"]