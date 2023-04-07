FROM node:14-alpine as build

WORKDIR /app

RUN npm install -g @angular/cli

COPY package*.json ./

RUN npm install

COPY . .

RUN npm run build

EXPOSE 4200

CMD app/node_modules/.bin/ng serve --host 0.0.0.0 --disableHostCheck